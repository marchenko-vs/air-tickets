import PlaneInfo from "../components/PlaneInfo";
import PlaneModel from "../models/PlaneModel";
import useGetAllPlanes from "../hooks/useGetAllPlanes";
import { apiUrl } from "../contexts/ConfigContext";

export default function Planes () {
  const [status, planes, setPlanes] = useGetAllPlanes(apiUrl, localStorage.getItem('jwt')!);

  return (
    <div className="planes-info">
      <h1 className="page-title">Самолеты нашей компании</h1>
      <div>
        { status === 200 ?
          planes.map((plane: PlaneModel) => {
            return <PlaneInfo key={plane.id} planeInfo={plane} />
          })
          : <p className='error-message'>Ошибка сервера</p>
        }
      </div>
    </div>
  )
}
