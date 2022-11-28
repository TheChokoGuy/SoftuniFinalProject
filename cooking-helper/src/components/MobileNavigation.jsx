import React, {useState} from 'react'
import NavLinks from './NavLinks.jsx'
import { Squash as Hamburger } from 'hamburger-react'

function MobileNavigation() {
    const [isOpen, setOpen] = useState(false)
    return (
      <>
      {isOpen &&<NavLinks className="navlinks"/>} 
    <nav className='mobile-navigation'>
          <h1>Celp</h1>
        <div>

        <Hamburger className='menu' size={32}
        onToggle={toggled => {
            if (toggled) {
               setOpen(!isOpen)
            } else {
                setOpen(!isOpen)
            }
          }} 
        />

        </div>


    </nav>
    

    </>
  )
}

export default MobileNavigation