import { useState } from 'react';
import './App.css';

function Square({ value, onSquareClick }: { value: string | null, onSquareClick: () => void }) {
    return (
        <button
            className="square"
            onClick={onSquareClick}
        >
            {value}
        </button>
    );
}

function calculateWinner(squares: (string | null)[]) {
    const lines = [
        [0, 1, 2],
        [3, 4, 5],
        [6, 7, 8],
        [0, 3, 6],
        [1, 4, 7],
        [2, 5, 8],
        [0, 4, 8],
        [2, 4, 6]
    ];
    for (let i = 0; i < lines.length; i++) {
        const [a, b, c] = lines[i];
        if (squares[a] && squares[a] === squares[b] && squares[a] === squares[c]) {
            return squares[a];
        }
    }
    return null;
}

export default function Game() {
    const [history, setHistory] = useState<(string | null)[][]>([Array(9).fill(null)]);
    const [currentMove, setCurrentMove] = useState(0);
    const xIsNext = currentMove % 2 === 0;
    const currentSquares = history[currentMove];

    function handlePlay(nextSquares: (string | null)[]) {
        const nextHistory = [...history.slice(0, currentMove + 1), nextSquares];
        setHistory(nextHistory);
        setCurrentMove(nextHistory.length - 1);
    }

    function jumpTo(index: number) {
        setCurrentMove(index);
    }

    const moves = history.map((squares, index) => {
        let description;
        if (index > 0) {
            description = 'Go to move #' + index;
        } else {
            description = 'Go to game start';
        }
        return (
            <li key={index}>
                <button onClick={() => jumpTo(index)}>{description}</button>
            </li>
        );
    });

    return (
        <div className="game">
            <div className="game-board">
                <Board xIsNext={xIsNext} squares={currentSquares} onPlay={handlePlay} />
            </div>
            <div className="game-info">
                <ol>{moves}</ol>
            </div>
        </div>
    );
}

interface BoardProps {
    xIsNext: boolean;
    squares: (string | null)[];
    onPlay: (nextSquares: (string | null)[]) => void;
}

function Board({ xIsNext, squares, onPlay }: BoardProps) {

    //const [squares, setSquares] = useState<(string | null)[]>(Array(9).fill(null));

    function handleClick(i: number) {
        if (squares[i] || calculateWinner(squares)) {
            return;
        }

        const nextSquares = squares.slice();
        if (xIsNext) {
            nextSquares[i] = 'X';
        }
        else {
            nextSquares[i] = 'O';
        }

        onPlay(nextSquares);
    }

    const winner = calculateWinner(squares);
    let status;
    if (winner) {
        status = "Winner: " + winner;
    } else {
        status = "Next player: " + (xIsNext ? "X" : "O");
    }

    return (
        <>
            <div className="status">{status}</div>
            <div className="board-row">
                <Square value={squares[0]} onSquareClick={() => handleClick(0)} />
                <Square value={squares[1]} onSquareClick={() => handleClick(1)} />
                <Square value={squares[2]} onSquareClick={() => handleClick(2)} />
            </div>
            <div className="board-row">
                <Square value={squares[3]} onSquareClick={() => handleClick(3)} />
                <Square value={squares[4]} onSquareClick={() => handleClick(4)} />
                <Square value={squares[5]} onSquareClick={() => handleClick(5)} />
            </div>
            <div className="board-row">
                <Square value={squares[6]} onSquareClick={() => handleClick(6)} />
                <Square value={squares[7]} onSquareClick={() => handleClick(7)} />
                <Square value={squares[8]} onSquareClick={() => handleClick(8)} />
            </div>
        </>
    );
}