import { createAction, props, createReducer, on } from "@ngrx/store";
import { Item } from "./item.model";

export const ItemRequest = createAction('[frontend to backed] request item/s');
export const ItemRequestSuccess = createAction('[frontend to backed] request item/s success', props<{ items: Item[] }>());
export const ItemRequestFailure = createAction('[frontend to backed] request item/s failure', props<{ errMsg: string }>());


export const toggleFavoriteItem= createAction('[favorite] toggle item', props<{ item: Item }>());


export interface FavoriteState {
  favorites : string[];
}

function toggleFavorite(list : string[], item : string) {
  if(list.length===0)
    return [item];
  if(list.includes(item))
    return list.filter(x=>x===item);
}

export const favoriteReducer = createReducer(
  { favorites: [] } ,
  on(toggleFavoriteItem, (s,a) => ({...s, favorites: toggleFavorite(s.favorites, a.item.id)}))
)
