
export interface mealDto {
  id: string;
  name: string;
  allergentList: Array<string>;
  allergentString: string;
  ingredientList: Array<string>;
  ingredientString: string;
  calories: number;
  vegan: boolean;
}
