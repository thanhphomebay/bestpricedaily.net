import { createAction, props } from "@ngrx/store";
import { Item } from "./item.model";

export const ItemRequest = createAction('[frontend to backed] request item/s');
export const ItemRequestSuccess = createAction('[frontend to backed] request item/s success', props<{ items: Item[] }>());
export const ItemRequestFailure = createAction('[frontend to backed] request item/s failure', props<{ errMsg: string }>());