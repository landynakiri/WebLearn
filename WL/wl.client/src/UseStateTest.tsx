import { useState } from 'react';

//useState update®t²§
export default function Counter() {
    const [number, setNumber] = useState(0);

    return (
        <>
            <h1>{number}</h1>
            <button onClick={() => {
                setNumber(n => n + 1);
                setNumber(n => n + 1);
                setNumber(n => n + 1);
            }}>+3</button>

            <h1>{number}</h1>
            <button onClick={() => {
                setNumber(number + 1);
                setNumber(number + 1);
                setNumber(number + 1);
            }}>+1</button>

            <h1>{number}</h1>
            <button onClick={() => {
                setNumber(number + 5);
                setNumber(n => n + 1);
                setNumber(42);
            }}>Increase the number</button>
        </>
    )
}
