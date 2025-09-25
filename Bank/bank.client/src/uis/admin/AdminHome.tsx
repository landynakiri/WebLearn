import { getUserApi } from '../../services/openApiGeneratorServices'
import { useEffect, useState } from 'react';
import type { GetUserResp } from '../../generated/models'
export default function AdminHome() {
    async function GetUsers() {
        const result = await getUserApi().usersGetUsers()  
        alert(result)
    }

    return (
        <div>
            <h1>Admin Home</h1>
            <p>Welcome, Admin! You can manage user accounts here.</p>
            <button onClick={() => GetUsers()}>GetUsersTestBtn</button>
            <UserTable></UserTable>
        </div>
    );
}

function UserTable() {
    const [users, setUsers] = useState<GetUserResp[]>([]);

    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const result = await getUserApi().usersGetUsers();
                setUsers(result);
            } catch (error) {
                console.error(error);
            }
        };
        fetchUsers();
    }, []);

    return (
        <ul>
            {users.map((u: GetUserResp, i) => (
                <li key={i}>{u.id}, {u.userName}, {u.email}, {u.roles}, {u.createdAt?.toDateString()}, { u.lastLogin?.toDateString()}</li>
            ))}
        </ul>
    );
}