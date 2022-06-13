import { mealDto } from '../../meals/api/mealsDtos'

export interface EditDietDTO {
  id: string;
  name: string;
  price: number;
  calories: number;
  vegan: boolean;
  description: string;
  veganString: string;
};

export interface SaveDietDTO {
  id: string;
  name: string;
  price: number;
  calories: number;
  vegan: boolean;
  description: string;
  meals: mealDto[];
};
