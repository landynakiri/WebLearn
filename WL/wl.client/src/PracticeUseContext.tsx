import { createContext, useContext } from "react";

type Theme = "light" | "dark" | "system";
const ThemeContext = createContext<Theme>("system");

const useGetTheme = () => useContext(ThemeContext);

export default function PracticeUseContext() {
    return (
        <>
            <div>
                <h1>Welcome to my PracticeUseContext</h1>
                <ThemeContext value={"dark"}>
                    <MyComponent></MyComponent>
                </ThemeContext>
            </div>            
        </>
    );
}

function MyComponent() {
        const theme = useGetTheme();
    return <div>Current theme is {theme}</div>;

}