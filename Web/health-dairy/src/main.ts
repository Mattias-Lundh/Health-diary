import App from "./App.svelte";

const app = new App({
  target: document.body,
  props: {},
});

export default app;

export type Consumption = {
  id: number;
  name: string;
  calories: number;
  protein: number;
  carbs: number;
  amount: number;
  unit: string;
  day: string;
};

export type ConsumptionTemplate = {
  id: number;
  templateName: string;
  name: string;
  calories: number;
  protein: number;
  carbs: number;
  amount: number;
  unit: string;
};

export type Excersise = {
  id: number;

  name: string;
  muscles: string[];
  duration: number;
  weight: number;
  reps: number;
  sets: number;
  day: string;
};
export type ExcersiseTemplate = {
  id: number;
  templateName: string;
  name: string;
  muscles: string[];
  duration: number;
  weight: number;
  reps: number;
  sets: number;
};

export type Meal = {
  id: number;
  name: string;
  foodItems: ConsumptionTemplate[];
};
