import Login from './Login';
import { login } from './services/authService';

function Start() {

    async function handleLogin(username: string, password: string) {
        const errMsg = await login(username, password);
        if (errMsg) {
            alert(errMsg);
        } else {
            alert("登入成功");
        }
    }

    return (
        <div>
            <h1>Bank</h1>
            <Login onLogin={handleLogin} />
        </div>
    );
}

export default Start;