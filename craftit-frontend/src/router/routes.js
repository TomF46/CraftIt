
const routes = [
  {
    path: '/',
    component: () => import('layouts/Layout.vue'),
    children: [
      { path: '', component: () => import('pages/Index.vue') },
      { path: 'products', component: () => import('pages/products/List.vue') },
      { path: 'products/add', component: () => import('pages/products/Add.vue') },
      { path: 'products/:id', component: () => import('pages/products/View.vue'), props: true },
      { path: 'products/:id/edit', component: () => import('pages/products/Edit.vue'), props: true }
    ]
  }
]

// Always leave this as last one
if (process.env.MODE !== 'ssr') {
  routes.push({
    path: '*',
    component: () => import('pages/Error404.vue')
  })
}

export default routes
