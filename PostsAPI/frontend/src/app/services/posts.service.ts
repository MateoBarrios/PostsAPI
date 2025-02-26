import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';

export interface Post {
  id: number;
  title: string;
  content: string;
  createdAt: string;
  categoryId: number;
  categoryName: string;
  comments: Comment[];
  category?: Category;
}

export interface Comment {
  id: number;
  text: string;
  createdAt: string;
  postId: number;
}

export interface PaginatedPosts {
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  posts: Post[];
}

export interface Category {
  id: number;
  name: string;
}

@Injectable({
  providedIn: 'root'
})
export class PostsService {
  private apiUrl = 'https://localhost:5001/api';
  private apiUrlPost = `${this.apiUrl}/posts`;
  private apiUrlComments = `${this.apiUrl}/comments`;

  constructor(private http: HttpClient) {}

  getPosts(pageNumber: number = 1, pageSize: number = 10): Observable<PaginatedPosts> {
    return this.http.get<PaginatedPosts>(`${this.apiUrlPost}?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  getPostById(id: number): Observable<Post> {
    return this.http.get<Post>(`${this.apiUrlPost}/${id}`);
  }

  createPost(post: FormData): Observable<Post> {
    return this.http.post<Post>(this.apiUrlPost, post);
  }

  updatePost(id: number, post: Partial<Post>): Observable<Post> {
    return this.http.put<Post>(`${this.apiUrlPost}/${id}`, post);
  }

  deletePost(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrlPost}/${id}`);
  }

  getCategories(): Observable<Category[]> {
    return this.http.get<{ $values: Category[] }>(`${this.apiUrl}/categories`).pipe(
      map(response => response.$values)
    );
  }

  getCommentsByPostId(postId: number): Observable<Comment[]> {
    return this.http.get<{ $values: Comment[] }>(`${this.apiUrlComments}/post/${postId}`)
      .pipe(
        map(response => response.$values ?? []) // Extraer el array correctamente
      );
  }
  

  addComment(postId: number, text: string): Observable<Comment> {
    return this.http.post<Comment>(this.apiUrlComments, { text, postId });
  }
}
