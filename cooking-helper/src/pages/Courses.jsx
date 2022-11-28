import Tab from'../components/Tab.jsx'

function Courses() {

  return (
    <div className="App">
      <Tab text="Basic Knife Skills" imagePath="./assets/Tabs/BasicKnifeSkills.jpg" path="skills"/>
      <Tab text="Meals" imagePath="./assets/Tabs/Meals.jpg" className="animate__bounceInLeft" path="meals"/>
    </div>
  )
}

export default Courses
