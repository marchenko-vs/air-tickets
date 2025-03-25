import { useNavigate } from "react-router-dom";
import OrderInfo from "../components/OrderInfo";
import useGetOrdersHistory from "../hooks/useGetOrdersHistory";
import OrderModel from "../models/OrderModel";
import { apiUrl } from "../contexts/ConfigContext";
import './OrdersHistory.css';

export default function OrdersHistory() {
  const jwt: string | null = localStorage.getItem('jwt');  
  
  const [orders, setOrders] = useGetOrdersHistory(apiUrl, jwt!);
  
  let navigate = useNavigate();

  const handleCheckDetails = (orderId: any) => {
    navigate(`/orders/${orderId}`);
  }
  
  return (
    <>
      <h1 className="page-title">История заказов</h1>
      <div>
        {
          orders.reverse().map((order: OrderModel) => {
            return (
              <div className="orders-history-container">
                <OrderInfo orderInfo={order} />
                {order.status == 'оплачен' ? <div className="orders-history-button-div">
                  <button className="orders-history-small-button" onClick={() => 
                  handleCheckDetails(order.id)}>Детали</button></div> : <></>}
              </div> 
            )
          })
        }
      </div>
    </>
  )
}
