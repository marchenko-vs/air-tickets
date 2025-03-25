import './App.css';
import Navbar from './components/Navbar';
import Planes from './pages/Planes';
import Flights from './pages/Home';
import Services from './pages/Services';
import Signup from './pages/Signup';
import Login from './pages/Login';

import { Route, Routes } from "react-router-dom";
import FoundFlights from './pages/Flights';
import PlaneDetails from './pages/PlaneDetails';
import Tickets from './pages/Tickets';
import { AuthContextProvider } from './contexts/AuthContext';
import Order from './pages/Order';
import Settings from './pages/Settings';
import ServicesForTicket from './pages/ServicesForTicket';
import OrdersHistory from './pages/OrdersHistory';
import PreviousOrder from './pages/PreviousOrder';

function App() {
  return (
    <>
      <AuthContextProvider>
        <Navbar />
        <Routes>
          <Route path="/" element={<Flights />} />
          <Route path="/flights" element={<FoundFlights />} />
          <Route path="/flights/:flightId/tickets" element={<Tickets />} />
          <Route path="/flights/:flightId/planes/:planeId" element={<PlaneDetails />} />
          <Route path="/planes" element={<Planes />} />
          <Route path="/services" element={<Services />} />
          <Route path="/users/signup" element={<Signup />} />
          <Route path="/users/login" element={<Login />} />
          <Route path="/users/settings" element={<Settings />} />
          <Route path="/orders/current" element={<Order />} />
          <Route path="/orders/current/tickets/:ticketId/services" element={<ServicesForTicket />}/>
          <Route path="/orders/history" element={<OrdersHistory />} />
          <Route path="/orders/:orderId" element={<PreviousOrder />} />
          <Route path="*" element={<Flights />} />
        </Routes>
      </AuthContextProvider>
      <footer></footer>
    </>
  );
}

export default App;
