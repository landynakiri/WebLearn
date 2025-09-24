import { getUserApi} from '../../services/openApiGeneratorServices'
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
        </div>
    );
}