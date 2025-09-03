import { useState } from 'react';

//export default function MovingDot() {
//    const [position, setPosition] = useState({
//        x: 0,
//        y: 0
//    });
//    return (
//        <div
//            onPointerMove={e => {
//                setPosition({ x: e.clientX, y: e.clientY });
//                //position.x = e.clientX;
//                //position.y = e.clientY;
//            }}
//            style={{
//                position: 'relative',
//                width: '100vw',
//                height: '100vh',
//            }}>
//            <div style={{
//                position: 'absolute',
//                backgroundColor: 'red',
//                borderRadius: '50%',
//                transform: `translate(${position.x}px, ${position.y}px)`,
//                left: -10,
//                top: -10,
//                width: 20,
//                height: 20,
//            }} />
//        </div>
//    );
//}

export default function Form() {
    const [person, setPerson] = useState({
        firstName: 'Barbara',
        lastName: 'Hepworth',
        email: 'bhepworth@sculpture.com'
    });

    function handleFirstNameChange(e) {
        //person.firstName = e.target.value;
        setPerson({
            ...person,
            firstName: e.target.value
        });
    }

    function handleLastNameChange(e) {
        //person.lastName = e.target.value;
        setPerson({
            ...person,
            lastName: e.target.value
        });
    }

    function handleEmailChange(e) {
        //person.email = e.target.value;
        setPerson({
            ...person,
            email: e.target.value
        });
    }

    return (
        <>
            <label>
                First name:
                <input
                    value={person.firstName}
                    onChange={handleFirstNameChange}
                />
            </label>
            <label>
                Last name:
                <input
                    value={person.lastName}
                    onChange={handleLastNameChange}
                />
            </label>
            <label>
                Email:
                <input
                    value={person.email}
                    onChange={handleEmailChange}
                />
            </label>
            <p>
                {person.firstName}{' '}
                {person.lastName}{' '}
                ({person.email})
            </p>
        </>
    );
}
