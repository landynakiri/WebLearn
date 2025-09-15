export async function login(username: string, password: string): Promise<string | null> {
    try {
        const response = await fetch('/login', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ username, password }),
        });
        if (response.ok) {
            return null; // 登入成功
        } else {
            const msg = await response.text();
            return msg || '登入失敗';
        }
    } catch {
        return '伺服器錯誤，請稍後再試';
    }
}

export async function registerAccount(username: string, password: string): Promise<string | null> {
    try {
        const response = await fetch('/register', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ username, password }),
        });
        if (response.ok) {
            return null; // 註冊成功
        } else {
            const msg = await response.text();
            return msg || '註冊失敗';
        }
    } catch {
        return '伺服器錯誤，請稍後再試';
    }
}