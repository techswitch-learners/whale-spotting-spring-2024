import { createContext, useState, useEffect } from "react"
import { useCookies } from "react-cookie"
import { BrowserRouter as Router, Routes, Route } from "react-router-dom"
import { CookieSetOptions } from "universal-cookie"
import NavbarTop from "./components/NavbarTop"
import About from "./pages/About"
import Error404 from "./pages/Error404"
import Faqs from "./pages/Faqs"
import Home from "./pages/Home"
import HotSpotView from "./pages/HotSpotView"
import Login from "./pages/Login"
import Logout from "./pages/Logout"
import Profile from "./pages/Profile"
import Register from "./pages/Register"
import SightingForm from "./pages/SightingForm"
import SightingView from "./pages/SightingView"
import SightingsSearch from "./pages/SightingsSearch"

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
              <Route path="/sightings" element={<SightingsSearch />} />
              <Route path="/sightings/add" element={<SightingForm />} />
              <Route path="/sightings/:id" element={<SightingView />} />
              <Route path="/hotspots/:id" element={<HotSpotView />} />
              <Route path="*" element={<Error404 />} />
            </Routes>
          </main>
        </Router>
      </BackgroundContext.Provider>
    </AuthContext.Provider>
  )
}

export default App
