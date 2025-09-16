import React, { useState } from 'react';

interface RegisterProps {
    onRegister?: (username: string, password: string) => void;
}

export default function RegisterPage(props: RegisterProps) {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        if (!username || !password) {
            setError('請輸入帳號和密碼');
            return;
        }
        setError('');
        if (props.onRegister) {
           props.onRegister(username, password);
        }
    };

    return (
        <div className="registerPage-container">
            <h2>註冊</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="username">帳號：</label>
                    <input
                        id="username"
                        type="text"
                        value={username}
                        onChange={e => setUsername(e.target.value)}
                        autoComplete="username"
                    />
                </div>
                <div>
                    <label htmlFor="password">密碼：</label>
                    <input
                        id="password"
                        type="password"
                        value={password}
                        onChange={e => setPassword(e.target.value)}
                        autoComplete="current-password"
                    />
                </div>
                {error && <div className="error">{error}</div>}
                <button type="submit">創建帳號</button>
            </form>
        </div>
    );
}