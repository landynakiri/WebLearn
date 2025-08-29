import { useReducer } from 'react';

type State = {
  count: number;
};

type Action =
  | { type: 'increment' }
  | { type: 'decrement' }
  | { type: 'reset' };

const initialState: State = { count: 0 };

function reducer(state: State, action: Action): State {
  switch (action.type) {
    case 'increment':
      return { count: state.count + 1 };
    case 'decrement':
      return { count: state.count - 1 };
    case 'reset':
      return initialState;
    default:
      return state;
  }
}

const UseReducerTest: React.FC = () => {
  const [state, dispatch] = useReducer(reducer, initialState);

  return (
    <div>
      <h2>useReducer 測試</h2>
      <p>計數：{state.count}</p>
      <button onClick={() => dispatch({ type: 'increment' })}>增加</button>
      <button onClick={() => dispatch({ type: 'decrement' })}>減少</button>
      <button onClick={() => dispatch({ type: 'reset' })}>重設</button>
    </div>
  );
};

export default UseReducerTest;