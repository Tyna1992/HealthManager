import './App.css'

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src="/heartlogo.png" alt="logo" width="70%" height="auto" />
        <h1>Welcome to the Health Manager App!</h1>
        <div id="toolbar">
          <button>Home</button>
          <button>Registration</button>
          <button>Login</button>
          <button>Search for activities</button>
          <button>Search for nutrients</button>
          <button>Search for recipes</button>
          <button>Search for drinks(?)</button>
        </div>
      </header>
    </div>
  )
}

export default App
