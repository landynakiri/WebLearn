import { useState } from 'react';
import Login from './Login';
import { getWeatherForecast, login, registerAccount } from './services/authService';
import RegisterPage from './RegisterPage';

function Start() {
    const [isRegisterPage, setIsRegisterPage] = useState(false);

    async function handleLogin(username: string, password: string) {
        const errMsg = await login(username, password);
        if (errMsg) {
            alert(errMsg);
        } else {
            alert("登入成功");
        }
    }

    async function handleRegister(username: string, password: string) {
        const errMsg = await registerAccount(username, password);
        console.log(import.meta.env.VITE_SOME_KEY) 
        if (errMsg) {
            alert(errMsg);
        } else {
            alert("註冊成功");
        }
    }

    async function handleGetWeather() {
         const result = await getWeatherForecast();
         if (typeof result === 'string') {
             alert(result); // 顯示錯誤訊息
         } else {
             alert(JSON.stringify(result, null, 2)); // 顯示天氣資料
         }
    }

    return (
        <div>
            <h1>Bank</h1>
            <button onClick={() => setIsRegisterPage(!isRegisterPage)}>{!isRegisterPage ?"註冊":"登入"}</button>
            {isRegisterPage ? <RegisterPage onRegister={handleRegister} /> : <Login onLogin={handleLogin} />}            
            <button onClick={() => handleGetWeather()}></button>
        </div>
    );
}

export default Start;