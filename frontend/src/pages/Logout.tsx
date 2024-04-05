import { useContext, useEffect } from "react"
import { Spinner } from "react-bootstrap"
import { useNavigate } from "react-router-dom"
import { AuthContext } from "../App"

const Logout = () => {
  const navigate = useNavigate()

  const authContext = useContext(AuthContext)

  useEffect(() => {
    authContext.removeCookie("token")
    setTimeout(() => {
      navigate("/")
    }, 500)
  }, [authContext, navigate])

  return (
    <div className="d-flex flex-column justify-content-center align-items-center" style={{ minHeight: "100%" }}>
      <Spinner />
      <p className="mt-3 mb-0">Logging out</p>
    </div>
  )
}

export default Logout
