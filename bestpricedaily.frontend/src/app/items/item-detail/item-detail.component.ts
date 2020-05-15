import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { ActivatedRoute } from '@angular/router';
import { switchMap, map, take, first } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Item } from '../item.model';
import { ItemService } from '../item-service';
import { AppState } from '../../app.store';
import { getCartCount, AddItemToCart } from '../../cart/cart.store';

@Component({
  selector: 'app-item-detail',
  templateUrl: './item-detail.component.html',
  styleUrls: ['./item-detail.component.css']
})
export class ItemDetailComponent implements OnInit {
  cartCount$ = this.store.pipe(select(getCartCount));
  item$: Observable<Item>;
  constructor(private store: Store<AppState>, private route: ActivatedRoute, private itemService: ItemService) { }


  ngOnInit(): void {
    this.item$ = this.route.params.pipe(
      switchMap(p =>
        this.itemService.get(p['id']))
      //   this.item$ = this.route.params.pipe(
      //     switchMap(p =>
      //       this.store.pipe(select(getCartItems)).pipe(
      //         map(items =>{debugger; return  items.find(x => x.uuid === p['id'])})
      //       )
      //     )
      //   )
      // }
    )
  }

  addItem(item: Item) {
    console.log(item);
    this.store.dispatch(AddItemToCart({ item }));
  }
}
