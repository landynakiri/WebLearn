import { useState } from 'react';
import Login from './Login';
import { login } from './services/authService';
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
        alert("註冊Test");
    }

    return (
        <div>
            <h1>Bank</h1>
            <button onClick={() => setIsRegisterPage(!isRegisterPage)}>{!isRegisterPage ?"註冊":"登入"}</button>
            {isRegisterPage ? <RegisterPage onRegister={handleRegister} /> : <Login onLogin={handleLogin} />}            
        </div>
    );
}

export default Start;