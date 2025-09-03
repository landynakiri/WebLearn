//import { useState } from 'react';
import { useImmer } from 'use-immer';

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

//export default function Form() {
//    const [person, setPerson] = useState({
//        firstName: 'Barbara',
//        lastName: 'Hepworth',
//        email: 'bhepworth@sculpture.com'
//    });

//    function handleFirstNameChange(e) {
//        //person.firstName = e.target.value;
//        setPerson({
//            ...person,
//            firstName: e.target.value
//        });
//    }

//    function handleLastNameChange(e) {
//        //person.lastName = e.target.value;
//        setPerson({
//            ...person,
//            lastName: e.target.value
//        });
//    }

//    function handleEmailChange(e) {
//        //person.email = e.target.value;
//        setPerson({
//            ...person,
//            email: e.target.value
//        });
//    }

//    return (
//        <>
//            <label>
//                First name:
//                <input
//                    value={person.firstName}
//                    onChange={handleFirstNameChange}
//                />
//            </label>
//            <label>
//                Last name:
//                <input
//                    value={person.lastName}
//                    onChange={handleLastNameChange}
//                />
//            </label>
//            <label>
//                Email:
//                <input
//                    value={person.email}
//                    onChange={handleEmailChange}
//                />
//            </label>
//            <p>
//                {person.firstName}{' '}
//                {person.lastName}{' '}
//                ({person.email})
//            </p>
//        </>
//    );
//}


//export default function Form() {
//    const [person, setPerson] = useState({
//        name: 'Niki de Saint Phalle',
//        artwork: {
//            title: 'Blue Nana',
//            city: 'Hamburg',
//            image: 'https://i.imgur.com/Sd1AgUOm.jpg',
//        }
//    });

//    function handleNameChange(e) {
//        setPerson({
//            ...person,
//            name: e.target.value
//        });
//    }

//    function handleTitleChange(e) {
//        setPerson({
//            ...person,
//            artwork: {
//                ...person.artwork,
//                title: e.target.value
//            }
//        });
//    }

//    function handleCityChange(e) {
//        setPerson({
//            ...person,
//            artwork: {
//                ...person.artwork,
//                city: e.target.value
//            }
//        });
//    }

//    function handleImageChange(e) {
//        setPerson({
//            ...person,
//            artwork: {
//                ...person.artwork,
//                image: e.target.value
//            }
//        });
//    }

//    return (
//        <>
//            <label>
//                Name:
//                <input
//                    value={person.name}
//                    onChange={handleNameChange}
//                />
//            </label>
//            <label>
//                Title:
//                <input
//                    value={person.artwork.title}
//                    onChange={handleTitleChange}
//                />
//            </label>
//            <label>
//                City:
//                <input
//                    value={person.artwork.city}
//                    onChange={handleCityChange}
//                />
//            </label>
//            <label>
//                Image:
//                <input
//                    value={person.artwork.image}
//                    onChange={handleImageChange}
//                />
//            </label>
//            <p>
//                <i>{person.artwork.title}</i>
//                {' by '}
//                {person.name}
//                <br />
//                (located in {person.artwork.city})
//            </p>
//            <img
//                src={person.artwork.image}
//                alt={person.artwork.title}
//            />
//        </>
//    );
//}



export default function Form() {
    const [person, updatePerson] = useImmer({
        name: 'Niki de Saint Phalle',
        artwork: {
            title: 'Blue Nana',
            city: 'Hamburg',
            image: 'https://i.imgur.com/Sd1AgUOm.jpg',
        }
    });

    function handleNameChange(e) {
        updatePerson(draft => {
            draft.name = e.target.value;
        });
    }

    function handleTitleChange(e) {
        updatePerson(draft => {
            draft.artwork.title = e.target.value;
        });
    }

    function handleCityChange(e) {
        updatePerson(draft => {
            draft.artwork.city = e.target.value;
        });
    }

    function handleImageChange(e) {
        updatePerson(draft => {
            draft.artwork.image = e.target.value;
        });
    }

    return (
        <>
            <label>
                Name:
                <input
                    value={person.name}
                    onChange={handleNameChange}
                />
            </label>
            <label>
                Title:
                <input
                    value={person.artwork.title}
                    onChange={handleTitleChange}
                />
            </label>
            <label>
                City:
                <input
                    value={person.artwork.city}
                    onChange={handleCityChange}
                />
            </label>
            <label>
                Image:
                <input
                    value={person.artwork.image}
                    onChange={handleImageChange}
                />
            </label>
            <p>
                <i>{person.artwork.title}</i>
                {' by '}
                {person.name}
                <br />
                (located in {person.artwork.city})
            </p>
            <img
                src={person.artwork.image}
                alt={person.artwork.title}
            />
        </>
    );
}
