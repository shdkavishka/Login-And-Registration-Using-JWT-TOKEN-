import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";

import LoginForm from './components/LoginForm';
import Register from "./components/Register";
import Appheader from "./components/Appheader";
import { NavMenu } from "./components/NavMenu";

const AppRoutes = [
  {
    index: true,
    element: <LoginForm />
    },

 
  {
    path: '/login',
    element: <LoginForm />
  },
  {
    path: '/navmenu',
    element: <NavMenu />
  },


  {
    path: '/appheader',
    element: <Appheader />
  },

    
  {
    path: '/register',
    element: <Register />
  },


  {
    path: '/login-form',
    element: <LoginForm />
  },

  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  }
];

export default AppRoutes;
