import { on, createAction, props, ActionReducerMap, createReducer, createFeatureSelector, createSelector, ActionReducer, MetaReducer } from "@ngrx/store";
import { CartState, cartReducer } from "./cart/cart.store";
import { localStorageSync } from 'ngrx-store-localstorage';
import { FavoriteState, favoriteReducer } from './items/item.store';

export const AddLoading = createAction('[loading] add', props<{ id: string }>());
export const RemoveLoading = createAction('[loading] remove', props<{ id: string }>());

export interface LoadingState {
    loadings: string[];
}

export interface AppState {
    favorite: FavoriteState,
    cart: CartState,
    loadings: LoadingState
}

const loadingReducer = createReducer(
    { loadings: [] },
    on(AddLoading, (s, a) => { const st = s.loadings.filter(str => str !== a.id); return { ...s, loadings: [...st, a.id] } }),
    on(RemoveLoading, (s, a) => ({ ...s, loadings: s.loadings.filter(str => str !== a.id) })),
);

export const appReducers: ActionReducerMap<AppState> = {
    favorite: favoriteReducer,
    cart: cartReducer,
    loadings: loadingReducer
}
// function serializedCart funct
export function localStorageSyncReducer(reducer: ActionReducer<any>): ActionReducer<any> {
    return localStorageSync({ keys : ['cart'], rehydrate : true})(reducer);
}
export const metaReducers: Array<MetaReducer<any, any>> = [localStorageSyncReducer];

const getLoadingState = createFeatureSelector<LoadingState>('loadings');

export const getIsLoading = createSelector(getLoadingState, s => s && s.loadings && s.loadings.length);
// export const getIsLoading = createSelector(getLoadingState, s => true);
