import { useContext, useEffect } from "react"
import { BackgroundContext } from "../App"
import Error404Whale from "../assets/Error404.gif"
import NavbarBottom from "../components/NavbarBottom"

const Error404 = () => {
  const backgroundContext = useContext(BackgroundContext)

  useEffect(() => {
    backgroundContext.setBackground(`url("${Error404Whale}")`)
  }, [backgroundContext])

  return (
    <>
      <div className="Home row min-h-100">
        <h1 className="text-center">404: Whale not found</h1>
      </div>
      <NavbarBottom />
    </>
  )
}
export default Error404
