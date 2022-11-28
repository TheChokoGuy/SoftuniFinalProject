import React from 'react'
import Header from './components/Header'
import Courses from './pages/Courses'
import Skills from './pages/Skills'
import Meals from './pages/Meals'
import Home from './pages/Home'
import {BrowserRouter as Router, Route, Switch} from 'react-router-dom'
import '../src/index.css'

function App() {

  return (
    <Router>
    <div className="App">
      <Header/>
      <div className='content'>
        <Switch>
          <Route exact path="/">
            <Home/>
          </Route>
          <Route exact path="/courses">
          <Courses/>
          </Route>
          <Route exact path="/skills">
            <Skills/>
          </Route>
          <Route exact path="/meals">
            <Meals/>
          </Route>
        </Switch>

      </div>
    </div>
    </Router>
  )
}

export default App
