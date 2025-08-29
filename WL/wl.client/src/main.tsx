import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import PracticeUseContext from './PracticeUseContext.tsx'

createRoot(document.getElementById('root')!).render(
    <StrictMode>
        <PracticeUseContext />
  </StrictMode>,
)
