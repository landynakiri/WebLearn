import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import CounterTest from './CounterTest.tsx'

createRoot(document.getElementById('root')!).render(
    <StrictMode>
        <CounterTest />
  </StrictMode>,
)
