import { Injectable, Inject } from '@angular/core';
import { ITEMS_MOCK } from './item.mocks';
import { Observable } from 'rxjs';
import { Item } from './item.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ItemService {
  baseUrl : string;
  constructor(@Inject('BASE_URL') baseUrl: string, private http: HttpClient) { this.baseUrl=baseUrl;}
  getAll(): Observable<Item[]> {
    // return ITEMS_MOCK;
    return this.http.get<Item[]>(this.baseUrl+'api/items');
  }
  get(id: string): Observable<Item> {
    // return ITEMS_MOCK.pipe(map(x => x.find(k => k.sku === id)));
    return this.http.get<Item>(this.baseUrl+'api/items/'+id);
  }
}
