import { Routes } from '@angular/router';
import { provideRouter } from '@angular/router';

export const routes: Routes = [
    {path: '', loadComponent:() => import('./pages/post-list/post-list.component').then(m => m.PostListComponent)},
    {path: 'post/:id', loadComponent:() => import('./pages/post-detail/post-detail.component').then(m => m.PostDetailComponent)},
    {path: 'new', loadComponent:() => import('./pages/post-form/post-form.component').then(m => m.PostFormComponent)},
    {path: '**', redirectTo: ''}
];

export const appConfig = [
    provideRouter(routes)
]
