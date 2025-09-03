import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import UseStateTest from './UseStateTest.tsx'

createRoot(document.getElementById('root')!).render(
    <StrictMode>
        <UseStateTest />
  </StrictMode>,
)
