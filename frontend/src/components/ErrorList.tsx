interface ErrorListProps {
  errors?: string[]
}

const ErrorList = ({ errors }: ErrorListProps) => {
  return (
    <>
      {errors && errors.length > 0 && (
        <ul className="list-unstyled mb-0 small">
          {errors.map((error) => (
            <li key={error} className="text-danger">
              {error}
            </li>
          ))}
        </ul>
      )}
    </>
  )
}

export default ErrorList
