import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Login from './uis/user/Login';
import { login, registerAccount } from './services/authService';
import RegisterPage from './uis/admin/RegisterPage';
import { weatherForecastApi } from './services/openApiGeneratorServices';


export default function Start() {
    const [isRegisterPage, setIsRegisterPage] = useState(false);
    const navigate = useNavigate();

    async function handleLogin(username: string, password: string) {

        const result = await login(username, password);
        if (!result.success) {
            alert(result.error);
        } else {
            localStorage.setItem('token', result.data.token);

            // 根據角色跳轉
            if (result.data.role === 'Admin') {
                navigate('/AdminHome');
            } else {
                navigate('/UserHome');
            }
        }
    }

    async function handleRegister(username: string, password: string) {
        const errMsg = await registerAccount(username, password);
        if (errMsg) {
            alert(errMsg);
        } else {
            alert("註冊成功");
        }
    }

    async function handleGetWeather() {   
        const result = await weatherForecastApi.getWeatherForecast();
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