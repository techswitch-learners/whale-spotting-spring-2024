# Whale Spotting

This is a project to help users find out more about whales and to encourage them to go and spot whales in the wild.

## Setting up

A database needs to be prepared, and all required dependencies need to be installed, before the project can be successfully run.

### Inside pgAdmin

First, create a user with the following credentials and permissions:

- Username `whales`
- Password `whales`
- Able to login

Then, create a database called `whales`, owned by the user created previously (also called `whales`).

### Inside the root directory

```bash
dotnet tool restore
npm install
```

### Inside the `backend/` directory

```bash
dotnet restore
dotnet ef database update
```

### Inside the `frontend/` directory

```bash
npm install
```

## Running the project

To run the project locally, the backend and frontend should be started separately.

### Inside the `backend/` directory

```bash
dotnet watch run
```

### Inside the `frontend/` directory

```bash
npm run dev
```
