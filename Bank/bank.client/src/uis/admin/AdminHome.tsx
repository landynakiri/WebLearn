import { getUserApi } from '../../services/openApiGeneratorServices'
import { useEffect, useState } from 'react';
import type { GetUserResp } from '../../generated/models'
export default function AdminHome() {
    return (
        <div>
            <h1>Admin Home</h1>
            <p>Welcome, Admin! You can manage user accounts here.</p>
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
        <table border={1} cellPadding={5}>
            <thead>
                <tr>
                    <th>ID</th>
                    <th>UserName</th>
                    <th>Email</th>
                    <th>Role</th>
                    <th>CreateAt</th>
                    <th>LastLogin</th>
                </tr>
            </thead>
            <tbody>
                {users.map(a => (
                    <tr key={a.id}>
                        <td>{a.id}</td>
                        <td>{a.userName}</td>
                        <td>{a.email}</td>
                        <td>
                            {a.roles?.join(", ")}
                        </td>
                        <td>{a.createdAt?.toDateString()}</td>
                        <td>{a.lastLogin?.toDateString()}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
}