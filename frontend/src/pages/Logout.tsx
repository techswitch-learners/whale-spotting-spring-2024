import { useContext, useEffect } from "react"
import { useNavigate } from "react-router-dom"
import { Spinner } from "react-bootstrap"
import { AuthContext, BackgroundContext } from "../App"

const Logout = () => {
  const navigate = useNavigate()

  const authContext = useContext(AuthContext)
  const backgroundContext = useContext(BackgroundContext)

  useEffect(() => {
    authContext.removeCookie("token")
    setTimeout(() => {
      navigate("/")
    }, 500)
  }, [authContext, navigate])

  useEffect(() => {
    backgroundContext.setBackground("white")
  }, [backgroundContext])

  return (
    <div className="d-flex flex-column justify-content-center align-items-center flex-grow-1">
      <Spinner />
      <p className="mt-3 mb-0">Logging out</p>
    </div>
  )
}

export default Logout
