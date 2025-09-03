import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import UpdateObjectTest from './UpdateObjectTest.tsx'

createRoot(document.getElementById('root')!).render(
    <StrictMode>
        <UpdateObjectTest />
  </StrictMode>,
)
