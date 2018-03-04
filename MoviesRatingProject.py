
# coding: utf-8

# In[4]:


import pandas as pd
movies = pd.read_csv('./pandas/movies.csv', sep=',')
print(type(movies))
movies.head(5)


# In[5]:


ratings=pd.read_csv('./pandas/ratings.csv',sep=',')
ratings.head(5)


# movies.isnull().any()

# In[20]:


ratings=ratings.dropna()
ratings.isnull().any()
movies.isnull().any()
ratings.pop('timestamp')
ratings.head(5)


# In[29]:


ratings.head(5)
finalt = movies.merge(ratings, on='movieId', how='inner')
finalt.head(5)


# In[49]:


MovieTRT=finalt.groupby('movieId',as_index=False).mean()
MovieTRT.pop('userId')


# In[50]:


MovieTRT.head(5)
MovieTRT.isnull().any()
len(MovieTRT)


# In[51]:


movies.head(5)
movies.isnull().any()
len(movies)


# In[63]:



MR=movies.merge(MovieTRT,on='movieId',how='inner')
MR=MR.sort_values(['rating'],ascending=[False])
MR.head(5)
MR.to_csv('out1.csv')


# In[70]:


#2 avg rating by user
ratings.head(5)
UT=ratings.groupby('userId',as_index=False).mean()
UT.pop('movieId')
UT.head(5)


# In[80]:


UT2=UT.sort_values(['rating'],ascending=[False])
UT2.head(5)


# In[88]:


movies.head(5)


# In[108]:


g=list(movies['genres'])
genres=[i.split('|') for i in g]
sets=set()
for i in genres:
    sets.update(i)
    
sets.remove('(no genres listed)')
sets
    


# In[120]:


finalt.head(5)
finalt.count()


# In[126]:


c={}
for i in sets:
    c[i]=finalt[finalt['genres'].str.contains(i)==True].count()
print(type(c))


# In[134]:


df3=pd.DataFrame(c)
df3


# In[190]:


#df3=df3.drop(df3.index[0])
df3=pd.DataFrame.transpose(df3)
df3




# In[156]:


#3 avg rating for genre
r={}
for i in sets:
    rat=finalt[finalt['genres'].str.contains(i)==True]
    r[i]=rat['rating'].mean()


# In[180]:


r


# In[197]:


r#3
dfr = pd.DataFrame([r], columns=r.keys())
dfr=pd.DataFrame.transpose(dfr)
dfr


# In[151]:


r=finalt[finalt['genres'].str.contains('Horror')==True]

r.head(5)


# In[153]:


rat=r['rating'].mean()
rat


# In[159]:


MR.to_csv('out1.csv')


# In[160]:


UT2.to_csv('out2.csv')


# In[191]:


df3.to_csv('out3.csv')


# In[198]:


dfr.to_csv('out4.csv')

