import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemmberListComponent } from './members/memmber-list/memmber-list.component';
import { MemmberDetailComponent } from './members/memmber-detail/memmber-detail.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { authGuard } from './_guards/auth.guard';

export const routes: Routes = [
    {path: '',component:HomeComponent},
    {
        path:'',
        runGuardsAndResolvers: 'always',
        canActivate: [authGuard],
        children:[
            {path: 'members',component:MemmberListComponent},
            {path: 'members/:id',component:MemmberDetailComponent},
            {path: 'lists',component:ListsComponent},
            {path: 'messages',component:MessagesComponent}
        ]
    },
    {path: '**',component:HomeComponent, pathMatch:'full'}
];
