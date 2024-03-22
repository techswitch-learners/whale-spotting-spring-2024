import { createContext, useState, useEffect } from "react"
import { BrowserRouter as Router, Routes, Route } from "react-router-dom"
import { useCookies } from "react-cookie"
import { CookieSetOptions } from "universal-cookie"
import NavbarTop from "./components/NavbarTop"
import NavbarBottom from "./components/NavbarBottom"
import Home from "./pages/Home"
import About from "./pages/About"
import Faqs from "./pages/Faqs"
import Login from "./pages/Login"
import Register from "./pages/Register"
import Profile from "./pages/Profile"
import Logout from "./pages/Logout"
import SightingForm from "./pages/SightingForm"
import AllSightings from "./pages/AllSightings/AllSightings"

export const AuthContext = createContext<{
  cookie: { token?: string }
  setCookie: (name: "token", value: string, options?: CookieSetOptions | undefined) => void
  removeCookie: (name: "token", options?: CookieSetOptions | undefined) => void
}>({
  cookie: {},
  setCookie: () => {},
  removeCookie: () => {},
})

export const BackgroundContext = createContext<{
  background: string
  setBackground: (background: string) => void
}>({
  background: "white",
  setBackground: () => {},
})

const App = () => {
  const [cookie, setCookie, removeCookie] = useCookies(["token"])

  const [background, setBackground] = useState("white")

  useEffect(() => {
    document.body.style.background = background
  }, [background])

  return (
    <AuthContext.Provider value={{ cookie, setCookie, removeCookie }}>
      <BackgroundContext.Provider value={{ background, setBackground }}>
        <Router>
          <NavbarTop />
          <main className="container-fluid">
            <Routes>
              <Route index element={<Home />} />
              <Route path="/about" element={<About />} />
              <Route path="/faqs" element={<Faqs />} />
              <Route path="/login" element={<Login />} />
              <Route path="/register" element={<Register />} />
              <Route path="/profile" element={<Profile />} />
              <Route path="/logout" element={<Logout />} />
              <Route path="/sightings" element={<AllSightings />} />
              <Route path="/sightings/add" element={<SightingForm />} />
            </Routes>
          </main>
          <NavbarBottom />
        </Router>
      </BackgroundContext.Provider>
    </AuthContext.Provider>
  )
}

export default App
