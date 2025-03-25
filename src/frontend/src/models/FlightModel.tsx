import React from "react";

export interface FlightModel {
    id: number;
    planeId: number;
    departurePoint: string;
    arrivalPoint: string;
    departureDateTime: string;
    arrivalDateTime: string;
}

export default FlightModel;
