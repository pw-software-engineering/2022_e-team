import { mealDto } from '../../meals/api/mealsDtos'

export interface EditDietDTO {
  id: string;
  calories: number;
  vegan: boolean;
  description: string;
  veganString: string;
};

export interface SaveDietDTO {
  id: string;
  calories: number;
  vegan: boolean;
  description: string;
  meals: mealDto[];
};
