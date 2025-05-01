import pandas as pd
from sklearn.model_selection import train_test_split
from sklearn.ensemble import RandomForestClassifier
from sklearn.pipeline import Pipeline
from sklearn.compose import ColumnTransformer
from sklearn.preprocessing import OneHotEncoder
import joblib

# Veriyi yükle
df = pd.read_csv("heart.csv")

X = df.drop("HeartDisease", axis=1)
y = df["HeartDisease"]

# Kategorik ve sayısal sütunları ayır
# Kategorik ve sayısal sütunları ayır
categorical = ["Sex", "ChestPainType", "RestingECG", "ExerciseAngina", "ST_Slope"]
numerical = [col for col in X.columns if col not in categorical]

# Pipeline
preprocessor = ColumnTransformer([
    ("cat", OneHotEncoder(), categorical)
], remainder='passthrough')

pipeline = Pipeline([
    ("preprocessor", preprocessor),
    ("classifier", RandomForestClassifier(n_estimators=100, random_state=42))
])

# Eğitim
X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)
pipeline.fit(X_train, y_train)

# Kaydet
joblib.dump(pipeline, "heart_pipeline2.pkl")
