import React from "react";

interface ServiceModel {
    id: number;
    name: string;
    price: number;
    economyClass: boolean;
    businessClass: boolean;
    firstClass: boolean;
}

export default ServiceModel;
