import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import Start from './Start.tsx'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import UserHome from './uis/user/UserHome.tsx'

createRoot(document.getElementById('root')!).render(
    <StrictMode>
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Start />} />
                <Route path="/UserHome" element={<UserHome />} />
            </Routes>            
        </BrowserRouter>
  </StrictMode>,
)
