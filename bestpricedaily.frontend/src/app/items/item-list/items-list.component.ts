import { Component, OnInit } from '@angular/core';
import { ITEMS_MOCK } from '../item.mocks';
import { Observable, of } from 'rxjs';
import { Item } from '../item.model';
import { Store, select } from '@ngrx/store';
import { ItemService } from '../item-service';
import { uuidv4 } from '../uuid';
import { switchMap, catchError } from 'rxjs/operators';
import { AppState, getIsLoading, AddLoading, RemoveLoading } from '../../app.store';
import { AddItemToCart, RemoveItemFromCart } from '../../cart/cart.store';
import { CartItem } from '../../cart/cart-item.model';

@Component({
  selector: 'app-items-list',
  templateUrl: './items-list.component.html',
  styleUrls: ['./items-list.component.css'],
})
export class ItemsListComponent implements OnInit {
  items: Item[];
  isLoading$ = this.store.pipe(select(getIsLoading));
  constructor(private store: Store<AppState>, private itemService: ItemService) { }

  ngOnInit(): void {
    // this.store.dispatch(AddLoading({ id: 'Items getAll' }));
    this.itemService.getAll().subscribe(
      res => this.items = res,
      // err => { this.store.dispatch(RemoveLoading({ id: 'Items getAll' })); console.log(err) },
      // () => this.store.dispatch(RemoveLoading({ id: 'Items getAll' }))
    )
  }

  addItem(item: Item) {
    console.log(item);
    this.store.dispatch(AddItemToCart({ item }));
  }
  delItem(item: CartItem) {
    this.store.dispatch(RemoveItemFromCart({ item }));
  }

  // list: Item[] = [
  //   { idx: 0, uuid: uuidv4(), price: 4, name: 'doll', desc: 'baby doll', pix: `https://api.adorable.io/avatars/100/${~~(Math.random() * 100)}` },
  //   { idx: 1, uuid: uuidv4(), price: 5, name: 'car', desc: 'car toy', pix: `https://api.adorable.io/avatars/100/${~~(Math.random() * 100)}` },
  //   { idx: 2, uuid: uuidv4(), price: 6, name: 'book', desc: 'my life', pix: `https://api.adorable.io/avatars/100/${~~(Math.random() * 100)}` },
  //   { idx: 3, uuid: uuidv4(), price: 6, name: 'book', desc: 'my life', pix: `https://api.adorable.io/avatars/100/${~~(Math.random() * 100)}` },
  // ];

}
