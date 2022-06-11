

export interface registerDto {

  password: string;

  client: clientDto;

  address: addressDto;

};

export interface clientDto {

  firstName: string;

  lastName: string;

  email: string;

  phone: string;
}

export interface addressDto {

  street: string;

  building: string;

  apartment: string;

  code: string;

  city: string;
}

export interface loginDto {

  email: string;

  password: string;

  userType: number;

};

export interface authDto {

  tokenJWT: string;

};

export interface ILoginData {
  email: string,
  password: string,
  userType: number;
}


