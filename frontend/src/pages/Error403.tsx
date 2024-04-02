import { useContext, useEffect } from "react"
import { BackgroundContext } from "../App"
import Error403Whale from "../assets/Error403.png"
import NavbarBottom from "../components/NavbarBottom"

const Error403 = () => {
  const backgroundContext = useContext(BackgroundContext)

  useEffect(() => {
    backgroundContext.setBackground(`url("${Error403Whale}")`)
  }, [backgroundContext])

  return (
    <>
      <div className="Home min-h-100 ">
        <h1 className="text-center" style={{ color: "gold", fontSize: "24px" }}>
          403: You are not authorised to view this page
        </h1>
      </div>
      <NavbarBottom />
    </>
  )
}
export default Error403
