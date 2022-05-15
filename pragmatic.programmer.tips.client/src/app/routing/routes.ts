import { Routes } from '@angular/router';

import { SubscribeComponent } from '../mailing-list/subscribe/subscribe.component';
import { UnsubscribeComponent } from '../mailing-list/unsubscribe/unsubscribe.component';

export const routes: Routes = [
  // { path: '', component: AppComponent, pathMatch: 'full' },
  // {
  //   path: '',
  //   loadChildren: () => import('./root.module').then((m) => m.RootModule),
  // },
  { path: '', component: SubscribeComponent, pathMatch: 'full' },
  { path: 's', component: SubscribeComponent, pathMatch: 'full' },
  { path: 'subscribe', component: SubscribeComponent, pathMatch: 'full' },
  { path: 'unsubscribe', component: UnsubscribeComponent, pathMatch: 'full' },
  { path: '**', component: SubscribeComponent, pathMatch: 'full' },
];
