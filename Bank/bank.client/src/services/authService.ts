export async function login(email: string, password: string): Promise<string | null> {
    try {
        const response = await fetch('/login', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ email, password }),
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

export async function registerAccount(email: string, password: string): Promise<string | null> {
    try {
        const response = await fetch('/register', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ email, password }),
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

export async function getWeatherForecast(): Promise<any[] | string> {
    try {
        const response = await fetch('/WeatherForecast', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
                // 如果需要授權，這裡可以加上 Authorization header
                // 'Authorization': `Bearer ${token}`
            }
        });
        if (response.ok) {
            return await response.text();
        } else {
            const msg = await response.text();
            return msg || '取得天氣資料失敗';
        }
    } catch {
        return '伺服器錯誤，請稍後再試';
    }
}