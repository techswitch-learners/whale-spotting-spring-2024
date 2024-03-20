import { createContext } from "react"
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
import Map from "./pages/map/Map"

export const AuthContext = createContext<{
  cookie: { token?: string }
  setCookie: (name: "token", value: string, options?: CookieSetOptions | undefined) => void
  removeCookie: (name: "token", options?: CookieSetOptions | undefined) => void
}>({
  cookie: {},
  setCookie: () => {},
  removeCookie: () => {},
})

const App = () => {
  const [cookie, setCookie, removeCookie] = useCookies(["token"])

  return (
    <AuthContext.Provider value={{ cookie, setCookie, removeCookie }}>
      <Router>
        <NavbarTop />
        <main className="container">
          <Routes>
            <Route index element={<Home />} />
            <Route path="/about" element={<About />} />
            <Route path="/faqs" element={<Faqs />} />
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
            <Route path="/map" element={<Map />} />
            <Route path="/profile" element={<Profile />} />
            <Route path="/logout" element={<Logout />} />
          </Routes>
        </main>
        <NavbarBottom />
      </Router>
    </AuthContext.Provider>
  )
}

export default App
