import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Login from './uis/user/Login';
import RegisterPage from './uis/admin/RegisterPage';
import { weatherForecastApi, userApi } from './services/openApiGeneratorServices';


export default function Start() {
    const [isRegisterPage, setIsRegisterPage] = useState(false);
    const navigate = useNavigate();

    async function handleLogin(username: string, password: string) {
        try {
            const result = await userApi.usersLogin({ loginRequest: { email: username, password: password } });
            const isAdmin = result.includes('Admin');

            //localStorage.setItem('token', result.data.token);

            if (isAdmin) {
                navigate('/AdminHome');
            } else {
                navigate('/UserHome');
            }
        } catch {
            alert("登入失敗");
        }
        
    }

    async function handleRegister(username: string, password: string) {
        try {
            await userApi.usersRegister({ registerRequest: { email: username, password: password } });
            alert("註冊成功");
        } catch (error) {
            alert("註冊失敗：" + (error instanceof Error ? error.message : String(error)));
        }
    }

    async function handleGetWeather() {   
        const result = await weatherForecastApi.weatherForecastGet();
        alert(result); // 顯示錯誤訊息
    }

    return (
        <div>
            <h1>Bank</h1>
            <button onClick={() => setIsRegisterPage(!isRegisterPage)}>{!isRegisterPage ?"註冊":"登入"}</button>
            {isRegisterPage ? <RegisterPage onRegister={handleRegister} /> : <Login onLogin={handleLogin} />}            
            <button onClick={() => handleGetWeather()}>GetWeatherTestBtn</button>
        </div>
    );
}